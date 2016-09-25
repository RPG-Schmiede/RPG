VAR fractions_points_pel = 0
VAR fractions_points_vf = 0
VAR fractions_points_eg = 0
VAR fractions_points_spc = 0





Willkommen. Ich möchte Dir ein paar Fragen stellen, um Dich besser kennen zu lernen. 
Fangen wir an. 
-> Frage_1
=== Frage_1 ===
Bist Du eher der coole Typ, oder die taffe Draufgängerin?
	* Ich bin der coolste Typ überhaupt! //(Junge)
	-> Frage_2
	* Draufgängerin trifft es ganz gut.  //(Mädchen)
	-> Frage_2
=== Frage_2 ===
Sehr gut. Und verrätst Du mir deinen Namen?
	*Namensfeld
	-> Frage_3
=== Frage_3 ===
Nun wird es etwas Interessanter. Wie gehst Du mit Problemen um?
	*Erstmal beobachte ich Alles aus der Ferne. //(Bogenschütze)
	-> Frage_4
	*Ich umgehe Probleme, wenn möglich. //(Dieb)
	-> Frage_4
	*Ein Problem löse ich direkt! //(Zweihändiger Kämpfer)
	-> Frage_4
	*Auf keinen Fall mit dem Kopf durch die Wand. //(Schild und Schwert)
	-> Frage_4
=== Frage_4 ===
Hier ist nun also ein Problem: Eine junge Person wird bedrängt und angegriffen.
	*Kein Problem! Die Typen mache ich locker fertig!
	~ fractions_points_vf = fractions_points_vf+2
	~ fractions_points_pel++
	-> Frage_5
	*Soll mich nicht stören.
	~ fractions_points_eg = fractions_points_eg+2
	~ fractions_points_pel++
	-> Frage_5
	*Ich bin mir ziemlich sicher, dass ich mit meiner Ausbildung helfen kann.
	~ fractions_points_pel = fractions_points_pel+2
	~ fractions_points_vf++
	-> Frage_5
	*Vielleicht sollte ich meine Kollegen um Hilfe bitten. 
	~ fractions_points_spc = fractions_points_spc+2
	~ fractions_points_vf++
	-> Frage_5
=== Frage_5 ===
Verstanden. Nun hat Jemand Schulden bei Dir. Welche, und wie forderst Du sie ein?
	*Er schuldet mir Geld und ich kann ziemlich nachdrücklich sein. 
	~ fractions_points_pel = fractions_points_pel+2
	-> Frage_6
	*Der Tropf schuldet mir sein Leben, das werde ich mir holen!
	~ fractions_points_eg = fractions_points_eg+2
	-> Frage_6
	*Es ist ein einfacher Gefallen, den ich einfordere, wenn ich es benötige.
	~ fractions_points_vf = fractions_points_vf+2
	-> Frage_6
	*Schulden? Ich habe gerne geholfen.
	~ fractions_points_spc = fractions_points_spc+2
	-> Frage_6
=== Frage_6 ===
Interessant. Du hast Streit mit einem Freund. 
	*Ich habe keine Freunde. 
	~ fractions_points_eg = fractions_points_eg+2
	-> Frage_7
	*Wir lösen das in einem fairen Kräftemessen. 
	~ fractions_points_pel = fractions_points_pel+2
	-> Frage_7
	*Ich bin sicher, wir können das ausdiskutieren. 
	~ fractions_points_spc = fractions_points_spc+2
	-> Frage_7
	*Wir streiten, aber wir vertragen uns auch wieder. 
	~ fractions_points_vf = fractions_points_vf+2
	-> Frage_7
=== Frage_7 ===
Bitte beende: Anderswesen sind....?
	*Nutztiere. 
	~ fractions_points_pel = fractions_points_pel+2
	-> Frage_8
	*Existent. 
	~ fractions_points_eg = fractions_points_eg+2
	-> Frage_8
	*interessant. Wir müssen sie erforschen um sie zu verstehen.
	~ fractions_points_spc = fractions_points_spc+2
	-> Frage_8
	*Genau so wie wir und wertvolle Bewohner dieser Welt.
	~ fractions_points_vf = fractions_points_vf+2
	-> Frage_8
=== Frage_8 ===
Jael droht damit, die Menschheit wieder auszulöschen.
	*Wir müssen ihn vernichten, für den Fortbestand der Menschen! 
	~ fractions_points_vf = fractions_points_vf+2
	~ fractions_points_pel++
	-> Frage_9
	*Niemand sollte so viel Macht haben! 
	~ fractions_points_pel = fractions_points_pel+2
	~ fractions_points_eg++
	-> Frage_9
	*Jael hat ganz Recht, die Menschen töten Ihre Welt. 
	~ fractions_points_spc = fractions_points_spc+2
	~ fractions_points_eg++
	-> Frage_9
	*Ist mir egal Die Menschen haben es verdient. 
	~ fractions_points_eg = fractions_points_eg+2
	~ fractions_points_spc++
	-> Frage_9
=== Frage_9 ===
Du bist alleine. 
	*Alleine ist ok. Ich konzentriere mich auf mein Training. 
	~ fractions_points_pel = fractions_points_pel+2
	-> Frage_10
	*Alleine?! Wo sind die Anderen?! 
	~ fractions_points_vf = fractions_points_vf+2
	-> Frage_10
	*Alleine muss doch nicht einsam bedeuten.
	~ fractions_points_spc = fractions_points_spc+2
	-> Frage_10
	*Besser ist es. Alleine heißt, dass weniger Menschen sterben müssen.
	~ fractions_points_eg = fractions_points_eg+2
	-> Frage_10
=== Frage_10 ===
Sehr gut. Dies ist die letzte Frage. Bitte wähle eine Farbe.
	*Rot, wie das Band, dass unsere Freundschaft symbolisiert.
	~ fractions_points_vf = fractions_points_vf+3
	-> Auswertung
	*Blau steht für Disziplin und lernbereitschaft. 
	~ fractions_points_pel = fractions_points_pel+3
	-> Auswertung
	*Weiß, wie die Weste der Forschung. //( 3 SCE)
	~ fractions_points_spc = fractions_points_spc+3
	-> Auswertung
	*Schwarz. Der Tod ist schwarz. //( 3 EG)
	~ fractions_points_eg = fractions_points_eg+3
	-> Auswertung
	
	
=== Auswertung ===

	{
	- fractions_points_pel > fractions_points_vf && fractions_points_pel > fractions_points_spc && fractions_points_pel > fractions_points_eg:
	-> PELEintrag
	- fractions_points_vf > fractions_points_pel && fractions_points_vf > fractions_points_spc && fractions_points_vf > fractions_points_eg:
	-> VFEintrag
	- fractions_points_spc > fractions_points_vf && fractions_points_spc > fractions_points_pel && fractions_points_spc > fractions_points_eg:
	-> SPCEintrag
	- else: 
	-> EGEintrag
}  
	
	
	
=== PELEintrag ===
	
Willkommen bei Platinum El.	
-> DONE
	
=== VFEintrag ===

Willkommen bei Vefom!	
-> DONE

=== SPCEintrag ===

Willkommen bei Splice!	
-> DONE

=== EGEintrag ===

You're Fucked.
-> DONE