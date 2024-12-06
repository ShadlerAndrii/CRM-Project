import axios from 'axios';

const API_URL = "https://localhost:7135/UserData";
const API_URL_AUTH = "https://localhost:7135/UserData/authenticate?";
export default {
    async createUser(Email, Password, Surname, Name, Phone, Birthday, Sex, Role) {
        let formData = new FormData();
        formData.append('newEmail', Email);
        formData.append('newPassword', Password);
        formData.append('newSurname', Surname);
        formData.append('newName', Name);
        formData.append('newPhone', Phone);
        formData.append('newBirthaday', Birthday);
        formData.append('newSex', Sex);
        formData.append('newRole', Role);

        await axios.post(API_URL, formData)
    },
    async loginUser(Email, Password) {
        let data = '';

        await axios.get(API_URL_AUTH + `email=${Email}&password=${Password}`).then(
            (response) => {
                data=response.data;
            }
        );
        
        return data;
    }
}
