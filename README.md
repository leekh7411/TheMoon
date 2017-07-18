# TheMoon
AWS-chatbot challenge 2017. Unity &amp; AWS Lex , Lambda..etc
# aws-lex-convo-bot-example
Reference implementation on building a conversational Amazon Lex Bot.


MoviePedia 
==========

Moviepedia ChatBot 
==================

Moviepedia Bot is a chat bot that helps you query information about a
movie of your choice. We will be using AWS Lambda, which supports either
of Node (0.10 or 4.3), Python (2.7), or Java (8) runtimes. This bot has
been written in NodeJS and utilizes.
[TMDB](https://www.themoviedb.org/?language=en) for quering movies and
returing desired information.

Steps to build the Moviepedia Bot 
---------------------------------

Step 1: Create the AWS Lambda function 
--------------------------------------

Use this end point to upload the lambda code from s3:
https://s3-us-west-2.amazonaws.com/re-invent-botworkshop/samples/moviePedia/moviePedia.zip

Instructions: 
-------------

Zip the code that you downloaded. Note compress the files together not
the folder. Now, go to AWS Lambda console. Create a new lambda function.
Select "Blank Fuction" as a blueprint. in "Configure triggers" section
press next. Now configure your lambda function.

##### Configure Function: 

      1. Name your lambda function : moviePediaLogic
      2. Add Description - Lambda function for Moviepedia bot logic
      3. Runtime - Node.js4.3
      4. Code Entry - Upload the zip you downloaded
      5. Handler Section - Leave as default
      6. Select an existing role -  lambda_basic_execution
      4. Set time to 30 secs

#### 5. Test your lambda function. 

Configure the following test event to test your lambda function.

    {
    "messageVersion": "1.0",
    "invocationSource": "FulfillmentCodeHook",
    "userId": "user-1",
    "sessionAttributes": {},
    "bot": {
    "name": "movieInfoApp",
    "alias": "$LATEST",
    "version": "$LATEST"
    },
    "outputDialogMode": "Text",
    "currentIntent": {
    "name": "movieInfo",
    "slots": {
      "name": "Suicide Squad",
      "summary": "Director"
      },
    "confirmationStatus": "None"
     }
    }

#### The output should look like this: 

    {
     "sessionAttributes": {},
     "dialogAction": {
     "type": "Close",
     "fulfillmentState": "Fulfilled",
     "message": {
       "contentType": "PlainText",
       "content": "Director of Suicide Squad is/are: David Ayer"
       }
     }
    }

Step 2: Creating your Bot 
-------------------------

#### Create Amazon Lex IAM Role: lex-exec-role 

Go to Identity and Access Management (IAM) console. In role name, use a
name that is unique within your AWS account (for example,
lex-exec-role).

In Select Role Type, choose AWS Service Roles, and then choose AWS
Lambda.

Note In the current implementation, Amazon Lex service role is not
available. Therefore, you first create a role using the AWS Lambda as
the AWS service role. After you create the role, you update the trust
policy and specify Amazon Lex as the service principal to assume the
role. In Attach Policy, choose Next Step (that is, you create a role
without any permissions).

Choose the role you created and update policies as follows:

In the Permissions tab, choose Inline Policies, and then attach the
following custom policy.

    { 
    "Version": "2012-10-17", 
    "Statement": [ 
    { 
      "Action": [ 
        "lambda:InvokeFunction"
      ], 
      "Effect": "Allow", 
      "Resource": "*" 
      } 
     ] 
    }

In the Trust Relationships tab, choose Edit Trust Relationship, and
specify the Amazon Lex service principal ("lex.amazonaws.com"). The
updated policy should look as shown:

    {
     "Version": "2012-10-17",
     "Statement": [
    {
      "Effect": "Allow",
      "Principal": {
        "Service": "lex.amazonaws.com"
      },
      "Action": "sts:AssumeRole"
      }
     ]
    }

#### 1. Create Amazon Lex Bot 
