/*package main

import (
	"fmt"
	"log"
	"os/exec"
)

func main() {
	out, err := exec.Command("tracert", "192.168.0.2").Output()
	if err != nil {
		log.Fatal(err)
	}
	fmt.Printf("The date is %s\n", out)
}
*/
package main

import (
	"log"
	"os/exec"
)

func main() {
	//cmd := exec.Command("print", "D:\\TestPrint\\testPrint.docx")
	cmd := exec.Command("ping", "192.168.0.2")
	err := cmd.Start()
	if err != nil {
		log.Fatal(err)
	}
	log.Printf("Waiting for command to finish...")
	err = cmd.Wait()
	log.Printf("Command finished with error: %v", err)
}
