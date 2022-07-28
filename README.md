
## Install
SQL Server 2022
Visual studio 2022
#### Using Package Manager Console in visual studio and run:

    update-database
## Run the app
The swagger page will display after run the application.

# REST API

The REST API to the app is described below.

## Get process

### Request

`api /process/`

    curl -i -H 'Accept: application/json' http://localhost:7000/api/process

### Response

    Code: 200
    Content: {
     “QuestPointsEarned”: [number],
     “TotalQuestPercentCompleted”: [number],
     “MilestonesCompleted”: [{ “MilestoneIndex”: [number], “ChipsAwarded”: [number] }]
        }