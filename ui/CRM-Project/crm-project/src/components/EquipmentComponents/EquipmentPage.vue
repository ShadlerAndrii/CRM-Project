<template>
    <button @click="currentPage = 'main'">Main</button>
    <div class="modal-body">
        <div id="createEquipment">
            <input type="text" placeholder="Your Equipment Model" v-model="Model">
            <select v-model="Type">
                <option value="" disabled>Choose equipment model</option>
                <option value="0">Tractor</option>
                <option value="1">Harvester</option>
                <option value="2">Seeder</option>
                <option value="3">Sprayer</option>
                <option value="4">Plow</option>
                <option value="5">Cultivator</option>
                <option value="6">Other</option>
            </select>
            <button @click="createSellersEquipments()">Create Equipment</button>
        </div>
        <h1>Your Equipment:</h1>
        <table>
            <thead>
                <tr>
                    <th>Model</th>
                    <th>Type</th>
                    <th>Change/Delete</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="equipment in Equipments.filter(row => row.OwnerId == sellerId)" :key="equipment.Id"
                    :id="equipment.Id">
                    <td><input id="Model" type="text" v-model="equipment.Model" disabled></td>
                    <td><input id="Type" type="text" v-model="equipment.Type" disabled></td>
                    <td>
                        <button @click="changeSellersEquipments(equipment.Id)"
                            v-if="equipment.Id != editingId">Change</button>
                        <button @click="saveSellersEquipments(equipment.Id, equipment.Model, equipment.Type)"
                            v-if="equipment.Id == editingId">Save</button>
                        <button @click="deleteSellersEquipments(equipment.Id)">Delete</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<style scoped>
/* Загальний стиль для модального вікна */
.modal-body {
    padding: 20px;
    background-color: #fff;
    border-radius: 8px;
    box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
    margin-top: 20px;
}

/* Заголовок */
h1 {
    text-align: center;
    margin-bottom: 20px;
    font-size: 24px;
    color: #333;
}

/* Поля вводу та селект для створення обладнання */
#createEquipment {
    display: flex;
    flex-direction: column;
    gap: 15px;
    margin-bottom: 20px;
}

/* Стиль для полів вводу */
input,
select {
    padding: 10px;
    font-size: 16px;
    border-radius: 5px;
    border: 1px solid #ddd;
    background-color: #f9f9f9;
}

input:focus,
select:focus {
    border-color: #007bff;
    outline: none;
}

/* Кнопка "Create Equipment" */
button {
    background-color: #007bff;
    color: white;
    padding: 10px 20px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    transition: background-color 0.3s ease;
    margin-top: 10px;
}

button:hover {
    background-color: #0056b3;
}

button:active {
    background-color: #004085;
}

/* Стиль для таблиці */
table {
    width: 100%;
    border-collapse: collapse;
    margin-top: 20px;
}

th,
td {
    padding: 10px;
    text-align: center;
    border: 1px solid #ddd;
}

th {
    background-color: #f4f4f4;
}

td input {
    background-color: #f9f9f9;
    border: 1px solid #ddd;
    text-align: center;
    width: 95%;
    padding: 5px;
    font-size: 14px;
    border-radius: 5px;
}

/* Вирівнювання кнопок в рядку таблиці */
td button {
    margin-left: 5px;
}

/* Кнопки "Change", "Save" і "Delete" */
button:disabled {
    background-color: #ccc;
    cursor: not-allowed;
}

/* Для кнопки "Change" та "Save" */
button.active {
    background-color: #28a745;
}

button.active:hover {
    background-color: #218838;
}

button.delete {
    background-color: #dc3545;
}

button.delete:hover {
    background-color: #c82333;
}

/* Для кнопки "Main" */
button.main-button {
    background-color: #17a2b8;
    margin-bottom: 20px;
    margin-left: 20px;
}

button.main-button:hover {
    background-color: #138496;
}

button.main-button:active {
    background-color: #117a8b;
}

/* Центрування кнопок */
button {
    display: inline-block;
}
</style>

<script>
import equipmentsAPI from '../../equipmentsAPI.js';
export default {
    props: ['localCurrentPage', 'localSellerId', 'localToken'],
    components: {
    },
    data() {
        return {
            Equipments: [],
            currentPage: this.localCurrentPage,
            sellerId: this.localSellerId,
            Model: '',
            Type: '',
            isEditing: false,
            editingId: '',
            UserToken: this.localToken,
            equipmentDictionary: {
                0: 'Tractor',
                1: 'Harvester',
                2: 'Seeder',
                3: 'Sprayer',
                4: 'Plow',
                5: 'Cultivator',
                6: 'Other'
            }
        };
    },
    watch: {
        localCurrentPage(newlocalCurrentPage) {
            this.currentPage = newlocalCurrentPage; // Оновлюємо локальну змінну, коли змінюється пропс
        },
        currentPage(newCurrentPage) {
            this.$emit('update:localCurrentPage', newCurrentPage); // Випромінюємо оновлене значення users
        }
    },
    methods: {
        async getSellersEquipments() {
            this.Equipments = (await equipmentsAPI.refreshData(this.UserToken)).slice();
            this.convertEquipment();
        },
        async createSellersEquipments() {
            await equipmentsAPI.createEquipment(this.sellerId, this.Model, this.Type, this.UserToken)
            this.resetValues();
            await this.getSellersEquipments();
        },
        async changeSellersEquipments(Id) {
            if (this.isEditing) {
                alert('Save first')
            }
            else {
                let inputs = Array.from(await document.getElementById(Id).getElementsByTagName('input'));
                inputs = inputs.filter(el => el.getAttribute('id') !== 'Type')
                inputs.forEach(el => el.disabled = false);

                this.isEdit = true;
                this.editingId = Id;
            }
        },
        async saveSellersEquipments(Id, Model, Type) {
            let inputs = Array.from(await document.getElementById(Id).getElementsByTagName('input'));
            inputs.forEach(el => el.disabled = true);

            this.isEdit = false;
            this.editingId = '';
            let reversedTupe = await this.reverseEquipment(Type)

            await equipmentsAPI.changeEquipment(Id, this.sellerId, Model, reversedTupe, this.UserToken);
            await this.getSellersEquipments();
        },
        async deleteSellersEquipments(Id) {
            await equipmentsAPI.deleteEquipment(Id, this.UserToken);
            await this.getSellersEquipments();
        },
        async resetValues() {
            this.Model = '',
                this.Type = '';
        },
        async convertEquipment() {
            for (let i = 0; i < this.Equipments.length; i++) {
                const equipment = this.Equipments[i];
                // Заміна CultureId на текстове значення
                const equipmentName = this.equipmentDictionary[equipment.Type];
                equipment.Type = equipmentName; // Додаємо нову властивість
            }
        },
        async reverseEquipment(equipmentName) {
            return Object.keys(this.equipmentDictionary).find(key => this.equipmentDictionary[key] === equipmentName);
        }
    },
    mounted: function () {
        this.getSellersEquipments();
    }
};
</script>