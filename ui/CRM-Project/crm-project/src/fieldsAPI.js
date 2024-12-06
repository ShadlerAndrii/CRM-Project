import axios from 'axios';

const API_URL = "https://localhost:7135/FieldData";
const API_URL_UD = "https://localhost:7135/FieldData/";
export default {
    async refreshData(token) {
        let data = [];
        await axios.get(API_URL, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        }).then(
            (response) => {
                data=response.data;
            }
        );
        return data;
    },
    async createField(OwnerId, Name, Size, Location, token) {
        let formData = new FormData();
        formData.append('newOwnerId', OwnerId);
        formData.append('newName', Name);
        formData.append('newSize', Size);
        formData.append('newLocation', Location);

        await axios.post(API_URL, formData, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
    },
    async changeField(Id, OwnerId, Name, Size, Location, token) {
        let formData = new FormData();
        formData.append('FieldId', Id);
        formData.append('changedOwnerId', OwnerId);
        formData.append('changedName', Name);
        formData.append('changedSize', Size);
        formData.append('changedLocation', Location);

        await axios.put(API_URL_UD + Id, formData, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
    },
    async deleteField(Id, token) {
        await axios.delete(API_URL_UD + Id), {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        };
    }
}