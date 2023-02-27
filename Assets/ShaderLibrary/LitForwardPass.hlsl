#ifndef UNIVERSAL_FORWARD_LIT_PASS_INCLUDED
    #define UNIVERSAL_FORWARD_LIT_PASS_INCLUDED

    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
    sampler2D _boneTexture;
    uint _boneTextureBlockWidth;
    uint _boneTextureBlockHeight;
    uint _boneTextureWidth;
    uint _boneTextureHeight;
    #if (SHADER_TARGET < 30 || SHADER_API_GLES)
        uniform float frameIndex;
        uniform float preFrameIndex;
        uniform float transitionProgress;
    #else
        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_DEFINE_INSTANCED_PROP(float, preFrameIndex)
        #define preFrameIndex_arr Props
        UNITY_DEFINE_INSTANCED_PROP(float, frameIndex)
        #define frameIndex_arr Props
        UNITY_DEFINE_INSTANCED_PROP(float, transitionProgress)
        #define transitionProgress_arr Props
        UNITY_INSTANCING_BUFFER_END(Props)
    #endif
    struct Attributes
    {
        float4 positionOS   : POSITION;
        float3 normalOS     : NORMAL;
        float4 tangentOS    : TANGENT;
        float2 texcoord     : TEXCOORD0;
        float2 lightmapUV   : TEXCOORD1;
        float4 positionWS:TEXCOORD2;
        float4 color:COLOR;
        UNITY_VERTEX_INPUT_INSTANCE_ID
    };

    struct Varyings
    {
        float2 uv                       : TEXCOORD0;
        DECLARE_LIGHTMAP_OR_SH(lightmapUV, vertexSH, 1);

        #if defined(REQUIRES_WORLD_SPACE_POS_INTERPOLATOR)
            float3 positionWS               : TEXCOORD2;
        #endif

        #ifdef _NORMALMAP
            float4 normalWS                 : TEXCOORD3;    // xyz: normal, w: viewDir.x
            float4 tangentWS                : TEXCOORD4;    // xyz: tangent, w: viewDir.y
            float4 bitangentWS              : TEXCOORD5;    // xyz: bitangent, w: viewDir.z
        #else
            float3 normalWS                 : TEXCOORD3;
            float3 viewDirWS                : TEXCOORD4;
        #endif

        half4 fogFactorAndVertexLight   : TEXCOORD6; // x: fogFactor, yzw: vertex light

        #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
            float4 shadowCoord              : TEXCOORD7;
        #endif

        float4 positionCS               : SV_POSITION;
        UNITY_VERTEX_INPUT_INSTANCE_ID
        UNITY_VERTEX_OUTPUT_STEREO
    };

    void InitializeInputData(Varyings input, half3 normalTS, out InputData inputData)
    {
        inputData = (InputData)0;

        #if defined(REQUIRES_WORLD_SPACE_POS_INTERPOLATOR)
            inputData.positionWS = input.positionWS;
        #endif

        #ifdef _NORMALMAP
            half3 viewDirWS = half3(input.normalWS.w, input.tangentWS.w, input.bitangentWS.w);
            inputData.normalWS = TransformTangentToWorld(normalTS,
            half3x3(input.tangentWS.xyz, input.bitangentWS.xyz, input.normalWS.xyz));
        #else
            half3 viewDirWS = input.viewDirWS;
            inputData.normalWS = input.normalWS;
        #endif

        inputData.normalWS = NormalizeNormalPerPixel(inputData.normalWS);
        viewDirWS = SafeNormalize(viewDirWS);
        inputData.viewDirectionWS = viewDirWS;

        #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
            inputData.shadowCoord = input.shadowCoord;
        #elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
            inputData.shadowCoord = TransformWorldToShadowCoord(inputData.positionWS);
        #else
            inputData.shadowCoord = float4(0, 0, 0, 0);
        #endif

        inputData.fogCoord = input.fogFactorAndVertexLight.x;
        inputData.vertexLighting = input.fogFactorAndVertexLight.yzw;
        inputData.bakedGI = SAMPLE_GI(input.lightmapUV, input.vertexSH, inputData.normalWS);
    }




    half4x4 loadMatFromTexture(uint frameIndex, uint boneIndex)
    {
        uint blockCount = _boneTextureWidth / _boneTextureBlockWidth;
        int2 uv;
        uv.y = frameIndex / blockCount * _boneTextureBlockHeight;
        uv.x = _boneTextureBlockWidth * (frameIndex - _boneTextureWidth / _boneTextureBlockWidth * uv.y);

        int matCount = _boneTextureBlockWidth / 4;
        uv.x = uv.x + (boneIndex % matCount) * 4;
        uv.y = uv.y + boneIndex / matCount;

        float2 uvFrame;
        uvFrame.x = uv.x / (float) _boneTextureWidth;
        uvFrame.y = uv.y / (float) _boneTextureHeight;
        half4 uvf = half4(uvFrame, 0, 0);

        float offset = 1.0f / (float) _boneTextureWidth;
        half4 c1 = tex2Dlod(_boneTexture, uvf);
        uvf.x = uvf.x + offset;
        half4 c2 = tex2Dlod(_boneTexture, uvf);
        uvf.x = uvf.x + offset;
        half4 c3 = tex2Dlod(_boneTexture, uvf);
        uvf.x = uvf.x + offset;
        //half4 c4 = tex2Dlod(_boneTexture, uvf);
        half4 c4 = half4(0, 0, 0, 1);
        //float4x4 m = float4x4(c1, c2, c3, c4);
        half4x4 m;
        m._11_21_31_41 = c1;
        m._12_22_32_42 = c2;
        m._13_23_33_43 = c3;
        m._14_24_34_44 = c4;
        return m;
    }
    half4 skinning(Attributes v)
    {
        half4 w = v.color;
        half4 bone = half4(v.positionWS.x, v.positionWS.y, v.positionWS.z, v.positionWS.w);
        #if (SHADER_TARGET < 30 || SHADER_API_GLES)
            float curFrame = frameIndex;
            float preAniFrame = preFrameIndex;
            float progress = transitionProgress;
        #else
            float curFrame = UNITY_ACCESS_INSTANCED_PROP(frameIndex_arr, frameIndex);
            float preAniFrame = UNITY_ACCESS_INSTANCED_PROP(preFrameIndex_arr, preFrameIndex);
            float progress = UNITY_ACCESS_INSTANCED_PROP(transitionProgress_arr, transitionProgress);
        #endif

        //float curFrame = UNITY_ACCESS_INSTANCED_PROP(frameIndex);
        int preFrame = curFrame;
        int nextFrame = curFrame + 1.0f;
        half4x4 localToWorldMatrixPre = loadMatFromTexture(preFrame, bone.x) * w.x;
        localToWorldMatrixPre += loadMatFromTexture(preFrame, bone.y) * max(0, w.y);
        localToWorldMatrixPre += loadMatFromTexture(preFrame, bone.z) * max(0, w.z);
        localToWorldMatrixPre += loadMatFromTexture(preFrame, bone.w) * max(0, w.w);

        half4x4 localToWorldMatrixNext = loadMatFromTexture(nextFrame, bone.x) * w.x;
        localToWorldMatrixNext += loadMatFromTexture(nextFrame, bone.y) * max(0, w.y);
        localToWorldMatrixNext += loadMatFromTexture(nextFrame, bone.z) * max(0, w.z);
        localToWorldMatrixNext += loadMatFromTexture(nextFrame, bone.w) * max(0, w.w);

        half4 localPosPre = mul(v.positionOS, localToWorldMatrixPre);
        half4 localPosNext = mul(v.positionOS, localToWorldMatrixNext);
        half4 localPos = lerp(localPosPre, localPosNext, curFrame - preFrame);

        half3 localNormPre = mul(v.normalOS.xyz, (float3x3) localToWorldMatrixPre);
        half3 localNormNext = mul(v.normalOS.xyz, (float3x3) localToWorldMatrixNext);
        v.normalOS = normalize(lerp(localNormPre, localNormNext, curFrame - preFrame));
        half3 localTanPre = mul(v.tangentOS.xyz, (float3x3) localToWorldMatrixPre);
        half3 localTanNext = mul(v.tangentOS.xyz, (float3x3) localToWorldMatrixNext);
        v.tangentOS.xyz = normalize(lerp(localTanPre, localTanNext, curFrame - preFrame));

        half4x4 localToWorldMatrixPreAni = loadMatFromTexture(preAniFrame, bone.x);
        half4 localPosPreAni = mul(v.positionOS, localToWorldMatrixPreAni);
        localPos = lerp(localPos, localPosPreAni, (1.0f - progress) * (preAniFrame > 0.0f));
        return localPos;
    }

    half4 skinningShadow(Attributes v)
    {
        half4 bone = half4(v.positionWS.x, v.positionWS.y,v.positionWS.z, v.positionWS.w);
        #if (SHADER_TARGET < 30 || SHADER_API_GLES)
            float curFrame = frameIndex;
            float preAniFrame = preFrameIndex;
            float progress = transitionProgress;
        #else
            float curFrame = UNITY_ACCESS_INSTANCED_PROP(frameIndex_arr, frameIndex);
            float preAniFrame = UNITY_ACCESS_INSTANCED_PROP(preFrameIndex_arr, preFrameIndex);
            float progress = UNITY_ACCESS_INSTANCED_PROP(transitionProgress_arr, transitionProgress);
        #endif
        int preFrame = curFrame;
        int nextFrame = curFrame + 1.0f;
        half4x4 localToWorldMatrixPre = loadMatFromTexture(preFrame, bone.x);
        half4x4 localToWorldMatrixNext = loadMatFromTexture(nextFrame, bone.x);
        half4 localPosPre = mul(v.positionOS, localToWorldMatrixPre);
        half4 localPosNext = mul(v.positionOS, localToWorldMatrixNext);
        half4 localPos = lerp(localPosPre, localPosNext, curFrame - preFrame);
        half4x4 localToWorldMatrixPreAni = loadMatFromTexture(preAniFrame, bone.x);
        half4 localPosPreAni = mul(v.positionOS, localToWorldMatrixPreAni);
        localPos = lerp(localPos, localPosPreAni, (1.0f - progress) * (preAniFrame > 0.0f));
        //half4 localPos = v.vertex;
        return localPos;
        // return v;
    }
    ///////////////////////////////////////////////////////////////////////////////
    //                  Vertex and Fragment functions                            //
    ///////////////////////////////////////////////////////////////////////////////

    // Used in Standard (Physically Based) shader
    Varyings LitPassVertex(Attributes input)
    {
        Varyings output = (Varyings)0;

        UNITY_SETUP_INSTANCE_ID(input);
        UNITY_TRANSFER_INSTANCE_ID(input, output);
        UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

        #ifdef UNITY_PASS_SHADOWCASTER
            input.positionOS = skinningShadow(input);
        #else
            input.positionOS = skinning(input);
        #endif

        VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
        VertexNormalInputs normalInput = GetVertexNormalInputs(input.normalOS, input.tangentOS);
        half3 viewDirWS = GetCameraPositionWS() - vertexInput.positionWS;
        half3 vertexLight = VertexLighting(vertexInput.positionWS, normalInput.normalWS);
        half fogFactor = ComputeFogFactor(vertexInput.positionCS.z);

        output.uv = TRANSFORM_TEX(input.texcoord, _BaseMap);

        #ifdef _NORMALMAP
            output.normalWS = half4(normalInput.normalWS, viewDirWS.x);
            output.tangentWS = half4(normalInput.tangentWS, viewDirWS.y);
            output.bitangentWS = half4(normalInput.bitangentWS, viewDirWS.z);
        #else
            output.normalWS = NormalizeNormalPerVertex(normalInput.normalWS);
            output.viewDirWS = viewDirWS;
        #endif

        OUTPUT_LIGHTMAP_UV(input.lightmapUV, unity_LightmapST, output.lightmapUV);
        OUTPUT_SH(output.normalWS.xyz, output.vertexSH);

        output.fogFactorAndVertexLight = half4(fogFactor, vertexLight);

        #if defined(REQUIRES_WORLD_SPACE_POS_INTERPOLATOR)
            output.positionWS = vertexInput.positionWS;
        #endif

        #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
            output.shadowCoord = GetShadowCoord(vertexInput);
        #endif

        output.positionCS = vertexInput.positionCS;

        return output;
    }

    // Used in Standard (Physically Based) shader
    half4 LitPassFragment(Varyings input) : SV_Target
    {
        UNITY_SETUP_INSTANCE_ID(input);
        UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

        SurfaceData surfaceData;
        InitializeStandardLitSurfaceData(input.uv, surfaceData);

        InputData inputData;
        InitializeInputData(input, surfaceData.normalTS, inputData);

        half4 color = UniversalFragmentPBR(inputData, surfaceData.albedo, surfaceData.metallic, surfaceData.specular, surfaceData.smoothness, surfaceData.occlusion, surfaceData.emission, surfaceData.alpha);

        color.rgb = MixFog(color.rgb, inputData.fogCoord);
        return color;
    }

#endif
