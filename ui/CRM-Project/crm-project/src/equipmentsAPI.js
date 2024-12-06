import axios from 'axios';

const API_URL = "https://localhost:7135/EquipmentData";
const API_URL_UD = "https://localhost:7135/EquipmentData/";
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
    async createEquipment(OwnerId, Model, Type, token) {
        let formData = new FormData();
        formData.append('OwnerId', OwnerId);
        formData.append('Model', Model);
        formData.append('Type', Type);

        await axios.post(API_URL, formData, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
    },
    async changeEquipment(Id, OwnerId, Model, Type, token) {
        let formData = new FormData();
        formData.append('EquipmentId', Id);
        formData.append('OwnerId', OwnerId);
        formData.append('changedModel', Model);
        formData.append('changedType', Type);

        await axios.put(API_URL_UD + Id, formData, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
    },
    async deleteEquipment(Id, token) {
        await axios.delete(API_URL_UD + Id, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
    }
}