# The Little Match Girl AR
An educational AR application based on Andersen's _The Little Match Girl_.

Demo video & details are available at: https://guizichen.wixsite.com/portfolio/ar-application


## Code Samples
(Detailed code explanations are included in the comments)

1. [**CharacterAction.cs**](https://github.com/Gavin-Guiii/The_Little_Match_Girl_AR/blob/main/CharacterAction.cs) <br>It reads all the existing appointments and displays them.



2. [**LightMatch.cs**](https://github.com/Gavin-Guiii/The_Little_Match_Girl_AR/blob/main/LightMatch.cs) <br> It handles CRUD (Create, Read, Update, Delete) of the appointment database, and provides other useful functions. For example, it can return data of user's next appointment chronologically, or return the latest PCR Test result.



3. [**DetectClickableObject.cs**](https://github.com/Gavin-Guiii/The_Little_Match_Girl_AR/blob/main/DetectClickableObject.cs) <br> It first reads all the medical centers stored in the medical center database, and then sorts them based on the distance between user's current location and the medical center.





