# Blackbird.io Matecat

Blackbird is the new automation backbone for the language technology industry. Blackbird provides enterprise-scale automation and orchestration with a simple no-code/low-code platform. Blackbird enables ambitious organizations to identify, vet and automate as many processes as possible. Not just localization workflows, but any business and IT process. This repository represents an application that is deployable on Blackbird and usable inside the workflow editor.

## Introduction

<!-- begin docs -->

Matecat is a free online CAT tool by Translated.

## Before setting up

Before you can connect you need to make sure that:

- You have a Matecat API key. See [this article](https://guides.matecat.com/obtaining-api-credentials) on how to obtain API credentials.

## Connecting

1. Navigate to apps and search for Matecat. If you cannot find Matecat then click _Add App_ in the top right corner, select Matecat and add the app to your Blackbird environment.
2. Click _Add Connection_.
3. Name your connection for future reference e.g. 'My Matecat'.
4. Enter your Matecat [API key](https://guides.matecat.com/obtaining-api-credentials).
5. Click _Authorize connection_.

![connecting](image/README/1693310380983.png)

## Actions

See the [Matecat API documentation](https://www.matecat.com/api/docs#/) for a detailed explanation on each action.

### Projects

- **Create project** creates a new project. You have to include all files, source language and target languages at once. It cannot be updated later. It returns the same information as _Get project_.
> Please note, the files you send to the 'Create project' action should have English names, without any other language characters or special symbols. Otherwise, Matecat may throw an unclear error
- **Get project** returns information about the project. This also includes word counts.
- **Cancel project** cancels the project.
- **Archive project** archives the project.
- **Activate project** activates the project.

### Jobs

- **Download job translated files** returns all translated files of this job.
- **Download job file as TMX** returns the TMX file representing the current translation memory.
- **Download job file as XLIFF** returns the XLIFF file representing the current translation memory.
- **Get job segments comments** returns a list of all comments that have been made in this job.
- **Get job** returns general job information.
- **Cancel job** cancels the job.
- **Archive job** archives the job.
- **Activate job** activates the job.
- **Assign job** assigns the job to a translator.

> Matecat does not have a job status that indicates whether it is in translated, translated, revised, etc. Instead, that needs to be deduced from the different job word counts. For convenience we have added a "Derived status" to the job model which can take have the following statusses: NEW, IN_TRANSLATION, TRANSLATED, IN_REVISION, REVISED. We have also added this property to the project model which will return the lowest of all the accumulated statusses of all its jobs.

### Translation issues

- **Get translation issue**.
- **Create translation issue**.
- **Delete translation issue**.
- **Get translation issue comments**.

### Glossaries

- **Import glossary**. Matecat doesn't support language codes without a country portion. If the glossary contains such languages, the most appropriate Matecat-supported language code is selected (e.g., _es-ES_ for the _es_ language code). Glossaries can only be imported as private TMs in Matecat. More details on glossary import specifics in Matecat can be found [here](https://guides.matecat.com/how-to-add-a-glossary).

## Events

- **On analysis completed** is triggered when a project analysis completes or when it fails. Use in combination with checkpoints.
- **On project status changed** is triggered when a project changes its derived status. F.e. if all jobs in a project change their status to translated.
- **On job status changed** is triggered when a job changes its derived status. F.e. when all the segments in a job are translated the status changes to translated.

## Example

![1729768345607](image/README/1729768345607.png)

In this example we see a translation being created from a Slack message and its attachments. When the Matecat analysis is complete we send a message with the link to Slack. Then when all the translations are completed we loop through all jobs and download the files. We then send the translated files back to Slack.

## Missing features

- Translation Versions
- Split jobs / chunks
- Full quality reports
- Project merging

Let us know if you are interested in these features!

## Feedback

Feedback to our implementation of Matecat is always very welcome. Reach out to us using the [established channels](https://www.blackbird.io/), or create an issue.

<!-- end docs -->
