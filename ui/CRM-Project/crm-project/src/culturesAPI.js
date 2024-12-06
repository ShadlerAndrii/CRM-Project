import axios from 'axios';

const API_URL = "https://localhost:7135/CultureData";
const API_URL_UD = "https://localhost:7135/CultureData/";
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
    async createCulture(OwnerId, Name, Amount, HarvestDate, token) {
        let formData = new FormData();
        formData.append('OwnerId', OwnerId);
        formData.append('newName', Name);
        formData.append('newAmount', Amount);
        formData.append('newHarvestDate', HarvestDate);

        await axios.post(API_URL, formData, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
    },
    async changeCulture(Id, OwnerId, Name, Amount, HarvestDate, token) {
        let formData = new FormData();
        formData.append('CultureId', Id);
        formData.append('OwnerId', OwnerId);
        formData.append('changedName', Name);
        formData.append('changedAmount', Amount);
        formData.append('changedHarvestDate', HarvestDate);

        await axios.put(API_URL_UD + Id, formData, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
    },
    async deleteCulture(Id, token) {
        await axios.delete(API_URL_UD + Id, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
    }
}