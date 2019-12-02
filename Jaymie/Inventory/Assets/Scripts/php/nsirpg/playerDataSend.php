<?php
//Server login Variables
	$server_name = "localhost";
	$server_username = "root";
	$server_password = "";
	$database_name = "nsirpg";
//User variables
	$username = $_POST["username"];
	$charName = $_POST["charName"];
	$skinIndex = $_POST["skinIndex"];
	$hairIndex = $_POST["hairIndex"];
	$eyesIndex = $_POST["eyesIndex"];
	$mouthIndex = $_POST["mouthIndex"];
	$clothesIndex = $_POST["clothesIndex"];
	$armourIndex = $_POST["armourIndex"];
	$strength = $_POST["strength"];
	$class = $_POST["class"];
	$points = $_POST["points"];
	$dexterity = $_POST["dexterity"];
	$constitution = $_POST["constitution"];
	$wisdom = $_POST["wisdom"];
	$intelligence = $_POST["intelligence"];
	$charisma = $_POST["charisma"];

//Check connection
	$conn = new mysqli($server_name, $server_username, $server_password, $database_name);
	if (!$conn)
	{
		die("Connection failed. ".mysqli_connect_error());
	}
	
	//Name Check
	$namecheckquery = "SELECT username FROM playerData WHERE username = '".$username."'";
	$namecheck = mysqli_query($conn,$namecheckquery);
	if (mysqli_num_rows($namecheck)>0)
	{
		echo "Username Already Exists";
		$updateuserquery = "UPDATE playerData SET charName = '".$charName."', 
		skinIndex = '".$skinIndex."', hairIndex = '".$hairIndex."', eyesIndex = '".$eyesIndex."', mouthIndex = '".$mouthIndex."', 
		clothesIndex ='".$clothesIndex."', armourIndex = '".$armourIndex."', class = '".$class."', points = '".$points."', strength = '".$strength."', dexterity = '".$dexterity."', constitution = '".$constitution."', 
		wisdom = '".$wisdom."', intelligence = '".$intelligence."',  charisma ='".$charisma."'WHERE username = '".$username."'";
		
		mysqli_query($conn, $updateuserquery) or die ("error insert failed");
		echo "Success";
		exit();
	}
	
	//Create Data
	$insertuserquery = "INSERT INTO playerData (username, charName, skinIndex, hairIndex, eyesIndex, mouthIndex, clothesIndex, armourIndex, class, points, strength, dexterity, constitution, wisdom, intelligence, charisma) VALUE ('".$username."', '".$charName."', '".$skinIndex."', '".$hairIndex."', '".$eyesIndex."', '".$mouthIndex."', '".$clothesIndex."', '".$armourIndex."', '".$class."', '".$points."', '".$strength."', '".$dexterity."', '".$constitution."', '".$wisdom."', '".$intelligence."', '".$charisma."');";
	mysqli_query($conn, $insertuserquery) or die ("error insert failed");
	echo "Success";
	
	
?>