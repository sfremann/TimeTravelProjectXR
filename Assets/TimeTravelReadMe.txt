--- LAYERS ---

Layers are exported in packages so you have to :
- set a new User Layer "Bloom" in "Edit/Project Settings/Tags and Layers"
- go to Project window and go to "Assets/Prefabs"
- select "InfoZone" and in the inspector change Layer to "Bloom" and apply this change to children as well
- go to Project window and go to "Assets/Rendering"
- select "UniversalRenderPipelineAsset_Renderer" and change a few settings
- in Filtering for both Opaque and Transparent Layer Masks, select all then uncheck the Bloom Layer
- in the Render Objects feature, in Filters Layer Mask select Bloom

--- URP SETTINGS ---

Set up URP for your project if it not already done, then :
- go to "Edit/Project Settings/Graphics" and set
- for the Scriptable Render Pipeline Settings select the "UniversalRenderPipelineAsset" located in "Assets/Rendering"
- go to "Edit/Project Settings/Quality" and set Render Pipeline Asset to "UniversalRenderPipelineAsset" located in "Assets/Rendering"