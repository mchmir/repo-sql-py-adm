import os


def manage_network_drive():
    # Request an action from the user
    action = input("Enter 'SWITCH' to connect the network drive or 'OFF' to disconnect: ").strip().upper()

    # Connection data
    network_path = r"\\10.0.4.64\mikhailchm"
    drive_letter = "Y:"

    if action == "SWITCH":
        # Connect a network drive
        try:
            os.system(f'net use {drive_letter} {network_path}')
            print(f"The network drive is connected to {drive_letter}")
        except Exception as e:
            print(f"Connection error: {e}")

    else:
        # Disconnect the network drive
        try:
            os.system(f'net use {drive_letter} /delete /y')
            print(f"Network disk {drive_letter} disabled")
        except Exception as e:
            print(f"Disconnect error: {e}")


if __name__ == "__main__":
    manage_network_drive()
