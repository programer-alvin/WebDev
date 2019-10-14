//script to drop and hide related menu without any conflict
var dropdown = document.getElementsByClassName("choice-chk");
for (let i = 0; i < dropdown.length; i++) {
    dropdown[i].addEventListener("change", function () {//adding event listener for change in checkboxes
        try {//try thes block elements since related is not always found
            this.classList.toggle("active");
            var dropdown_content = this.parentNode;//level up to name
            var choises_checkbox_elements = dropdown_content.getElementsByTagName("input");//get input elements
            var num_of_checked = 0;//initialize count to zero
            for (let j = 0; j < choises_checkbox_elements.length; j++) {//transverse through all choises elements
                if (choises_checkbox_elements[j].type == "checkbox" && choises_checkbox_elements[j].checked == true) {//check if the element is checkbox and cheched
                    num_of_checked++;//increment number of checked checkboxes
                    if (num_of_checked > 1) {//if the number of checked checkboxes is more than one the ignore the action new checkbox
                        return;//do nothing by returning in order to save CPU cycles created by for loop
                    }
                }
            }
            dropdown_content = dropdown_content.parentNode;//level up to choises
            dropdown_content = dropdown_content.nextElementSibling;//go next to related
            if (dropdown_content.classList.contains("related")) {//check the element belongs to class related else do nothing
                if (dropdown_content.children.length == 2) {//if length is not two then it means related class does not have clickable options
                    if (dropdown_content.style.display === "block") {//if elements are displayed
                        if (num_of_checked == 0) {//hide related menu only when their is no checkbox leftchecked
                            dropdown_content.style.display = "none";//hide

                            var choises_checkbox_elements = dropdown_content.getElementsByTagName("input");//get input elements
                            for (let j = 0; j < choises_checkbox_elements.length; j++) {//transverse through all related elements
                                if (choises_checkbox_elements[j].type == "checkbox" && choises_checkbox_elements[j].checked == true) {//check if the element is checkbox and cheched
                                    choises_checkbox_elements[j].checked = false;//uncheck the checkboxes
                                }
                            }
                            var class_choises_in_related = dropdown_content.getElementsByClassName("choices"); //find class name choices in related class
                            for (let j = 0; j < class_choises_in_related.length; j++) {//loop through all choices
                                class_choises_in_related[j].style = "none";//hide choices in the related class
                            }
                        }
                    } else {
                        dropdown_content.style.display = "block";// show items
                    }
                }
            }
        } catch (ex) { }
    });
}