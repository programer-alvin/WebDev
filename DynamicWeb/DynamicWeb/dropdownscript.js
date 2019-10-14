/* Loop through all dropdown buttons to toggle between hiding and showing its dropdown content - This allows the user to have multiple dropdowns without any conflict */
var dropdown = document.getElementsByClassName("dropdown-chk");
for (let i = 0; i < dropdown.length; i++) {
    dropdown[i].addEventListener("change", function () {//adding event listener for change in checkboxes
        this.classList.toggle("active");
        var dropdown_content = this.nextElementSibling;//go next to label
        dropdown_content = dropdown_content.nextElementSibling;//go next to choices
        if (dropdown_content.style.display === "block") {//if elements are displayed
            dropdown_content.style.display = "none";//hide items
            var choises_checkbox_elements = dropdown_content.getElementsByTagName("input");//get input elements
            for (let j = 0; j < choises_checkbox_elements.length; j++) {//transverse through all choises elements
                if (choises_checkbox_elements[j].type == "checkbox" && choises_checkbox_elements[j].checked == true) {//check if the element is checkbox and cheched
                    choises_checkbox_elements[j].checked = false;//uncheck the checkboxes
                }
            }
            try {// try because not allchoises have related especially at sub level
                dropdown_content = dropdown_content.nextElementSibling;//go next to related

                if (dropdown_content.classList.contains("related")) {
                    dropdown_content.style.display = "none";//hide related items
                    choises_checkbox_elements = dropdown_content.getElementsByTagName("input");//get input elements
                    for (let j = 0; j < choises_checkbox_elements.length; j++) {//transverse through all related elements
                        if (choises_checkbox_elements[j].type == "checkbox" && choises_checkbox_elements[j].checked == true) {//check if the element is checkbox and cheched
        
                            choises_checkbox_elements[j].checked = false;//uncheck the checkboxes

                        }
                    }
                //hide choises in related
                var name_child = dropdown_content.children[1];//second child is name
                var choice_child = name_child.children[2];//third child is choices
                if (choice_child.classList.contains("choices")) {
                    choice_child.style.display = "none";//hide choises in related items
                }

                }
            } catch (ex) {

            }

        } else {
            dropdown_content.style.display = "block";// show items
        }
    });
}