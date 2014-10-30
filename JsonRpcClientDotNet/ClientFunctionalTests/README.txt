Those tests require a running JSON-RPC service confogming to ClientTests.TestService interface.
It needs to be running on local Android emulator on port 1666.

Emulator port forwarding needs to be enabled using this Android Debug Bridge (adb) command:
adb forward tcp:1666 tcp:1666