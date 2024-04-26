# TimeTravelProjectXR

*Winter 2023*

![image](ReadMeResources/Preview.png)

## Concept

The project *TimeTravel* was developed as part of a research project between our university *Université de Technologie de Compiègne* and the memorial of Compiègne. The idea was to reconstruct in 3D and XR the history of this place through a virtual visit that could allow the users to see the place evolve and change with time. During the winter semester of 2023, we were able to test different approaches and finally be able to create an experience including four periods: 
- 1914-1918: military hospital for the French army,
- 1941-1944: internment camp during German occupation,
- 1945-1997: casern for the French army,
- 2008-present: memorial.

> [!NOTE]  
> Being a project for a French memorial the experience is in French.

> [!NOTE]  
> Being a university project this experience is not 100% finished nor optimised. The historical information might not be always accurate, think of it as a POC more than a finished product.

> [!NOTE] 
> 3D version for PC: [*TimeTravelProject*](https://github.com/sfremann/TimeTravelProject)

---

## How to use

> [!IMPORTANT]  
> Made to be used with an Oculus Meta Quest 3. It uses the Mixed Reality feature and is intended to be used in one of the rooms of the memorial. In case you want to use it somewhere else you need to find a large enough room or you will have to implement movement with controllers.

### ◻️ Gameplay

The game is developed for Oculus Meta Quest 3 and uses Mixed Reality. You can move inside the experience by physically moving through the place you are in.    
  
If you move inside a green or yellow glowing circle you will be able to listen to some historical information. If the voice stops and you move out of the circle it will turn blue, meaning there is nothing left to listen in this zone. If you step out of the circle while the audio is playing the circle will remain yellow, meaning you can get inside again to finish the audio you started. A green zone is a zone you have never been in yet. Once all zones are either yellow or blue you can come closer to the picture close to the window and move to the next period.

To exit go to Meta Quest menu and quit the application.

### ◻️ Modifying the project

The project is built in a modular way so it is relatively easy to add more periods, remove periods, modify periods, etc... The *`PeriodManager`* manages the implemented periods. You can add or remove a period here. For a new period to work properly you will need to make sure the new period has the right architecture.

![image](ReadMeResources/PeriodManager.png)

For each period a *`RenderSettingsManager`* handles the information / audio zones in this period. You can add or remove one. All zones have the same structure from the prefab *`InfoZone`*, you only need to select the right audio and place the zone in the room.

![image](ReadMeResources/RenderSettingsManager.png)

![image](ReadMeResources/SoundZones.png)

Each period contains an introductory audio activated upon arrival in the period.

![image](ReadMeResources/PeriodIntroduction.png)

---

## Credits

- [Sarah Frémann](https://github.com/sfremann)
- [Anaïs Bourguignon](https://github.com/plinkakko)


### ◻️ Design & Writing

- **Scenario:** Sarah Frémann & Anaïs Bourguignon
- **Dialogue:** Sarah Frémann
- **Historical research:** Sarah Frémann & Anaïs Bourguignon

### ◻️ Programming & Engineering

- **Gameplay:** Sarah Frémann
- **Environment behavior:** Sarah Frémann
- **Putting things together / Solving conflict:** mostly Sarah Frémann with help from Anaïs Bourguignon
- **Testing / Debug:** mostly Sarah Frémann with help from Anaïs Bourguignon

### ◻️ Visuals

- **Effects & transitions / shaders:** Sarah Frémann
- **Models & environment:** Anaïs Bourguignon

### ◻️ Audio 

- **Sound selection:** Sarah Frémann & Anaïs Bourguignon
- **Effects & transitions:** Sarah Frémann
- **Voice & recording:** Sarah Frémann

<details>
  <summary>Technologies & resources</summary>

  - Unity 2022.3.4f1
  - Audacity
  - Blender
</details>

> [!NOTE] 
> Most sounds except for the voice are free-to-use assets we found on Pixabay. A few extra models are free-to-use assets we found on Sketchfab or Unity Asset Store, but most models were made by Anaïs Bourguignon.

> [!NOTE] 
> The pencil effect is based on the work of [BattleDawnNZ](https://github.com/BattleDawnNZ/Image-Effects-with-Shadergraph). The shaders and effects were modified to fit the needs of this project. Below are some early tests with the shaders.

![image](ReadMeResources/Shader.png)

---