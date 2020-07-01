1. 
	domain : MessageChecker (message consumer) ==> queueMessageConsumer-ejb
	mdp : amessage
	mdp master: amessage
	port : 10 000
	admin port : 10048

2. 
	domain : MessageReceiverJMS (webservice) ==> messageReceiver-ejb
	mdp : areceiver
	mdp master : areceiver
	port : 12 000
	admin port : 12 048
	Provider PORT : 12 076
3. 
	Database global name : frenchDictionnary
	SID : frenchDictio
	Database psw : Cesi123!
