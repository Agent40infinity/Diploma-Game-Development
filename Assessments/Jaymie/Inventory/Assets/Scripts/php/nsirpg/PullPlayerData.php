<?php
// Server Login variables
	$server_name = "localhost";
	$server_username = "root";
	$server_password = "";
	$database_name = "nsirpg";
	
	$username = $_POST["username"];
	
// Check connection
	$conn = new mysqli($server_name, $server_username, $server_password, $database_name);
	if(!$conn)
	{
		die("Connection Failed. ".mysqli_connect_error());
	}	
	$checkAccount = "SELECT username,charName, skinIndex, hairIndex, eyesIndex, mouthIndex, clothesIndex, armourIndex, class, points, strength, dexterity, constitution, wisdom, intelligence, charisma FROM playerData WHERE username = '".$username."';";
	$checkResult = mysqli_query($conn,$checkAccount);
	
	
// Check for Account
	if(mysqli_num_rows($checkResult) > 0)
	{
		while($row = mysqli_fetch_assoc($checkResult))
		{
			echo $row['charName'] . "|" . $row['skinIndex'] . "|" . $row['hairIndex'] . "|" . $row['eyesIndex'] . "|" . $row['mouthIndex'] . "|" . $row['clothesIndex'] . "|" . $row['armourIndex'] . "|" . $row['class'] . "|" . $row['points'] . "|" . $row['strength'] . "|" . $row['dexterity'] . "|" . $row['constitution'] . "|" . $row['wisdom'] . "|" . $row['intelligence'] . "|" . $row['charisma'] . "";
			return;
		}
	}
	else
	{
		echo "User Not Found";
		return;
	}
?>