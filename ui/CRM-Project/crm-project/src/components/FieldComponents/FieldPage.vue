<template>
    <button @click="currentPage = 'main'">Main</button>
    <div class="modal-body">
        <div id="createField">
            <input type="text" placeholder="Your Field Name" v-model="Name">
            <input type="text" placeholder="Your Field Size" v-model="Size" @input="validateInput('Size')">
            <input type="text" placeholder="Your Field Location" v-model="Location">
            <button @click="createSellersFields()">Create Field</button>
        </div>
        <h1>Your Fields:</h1>
        <table>
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Size</th>
                    <th>Location</th>
                    <th>Change/Delete</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="field in Fields.filter(row => row.OwnerId == sellerId)" :key="field.Id" :id="field.Id">
                    <td><input id="Name" type="text" v-model="field.Name" disabled></td>
                    <td><input id="Size" type="text" v-model="field.Size" disabled></td>
                    <td><input id="Price" type="text" v-model="field.Location" disabled></td>
                    <td>
                        <button @click="changeSellersFields(field.Id)" v-if="field.Id != editingId">Change</button>
                        <button @click="saveSellersFields(field.Id, field.Name, field.Size, field.Location)"
                            v-if="field.Id == editingId">Save</button>
                        <button @click="deleteSellersFields(field.Id)">Delete</button>
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

/* Поля вводу для створення поля */
#createField {
    display: flex;
    flex-direction: column;
    gap: 10px;
    margin-bottom: 20px;
}

/* Стиль для полів вводу */
input {
    padding: 10px;
    font-size: 16px;
    border-radius: 5px;
    border: 1px solid #ddd;
    background-color: #f9f9f9;
}

input:focus {
    border-color: #007bff;
    outline: none;
}

/* Кнопка "Create Field" */
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
import fieldsAPI from '../../fieldsAPI.js';
export default {
    props: ['localCurrentPage', 'localSellerId', 'localToken'],
    data() {
        return {
            Fields: [],
            currentPage: this.localCurrentPage,
            sellerId: this.localSellerId,
            Location: '',
            Name: '',
            Size: '',
            isEditing: false,
            editingId: '',
            UserTocken: this.localToken,
        };
    },
    watch: {
        localCurrentPage(newlocalCurrentPage) {
            this.currentPage = newlocalCurrentPage;
        },
        currentPage(newCurrentPage) {
            this.$emit('update:localCurrentPage', newCurrentPage);
        }
    },
    methods: {
        async getSellersFields() {
            this.Fields = (await fieldsAPI.refreshData(this.UserTocken)).slice();
        },
        async createSellersFields() {
            await fieldsAPI.createField(this.sellerId, this.Name, this.Size, this.Location, this.UserTocken);
            this.resetValues();
            this.getSellersFields();
        },
        async changeSellersFields(Id) {
            if (this.isEditing) {
                alert('Save first');
            } else {
                let inputs = Array.from(await document.getElementById(Id).getElementsByTagName('input'));
                inputs.forEach(el => el.disabled = false);

                this.isEditing = true;
                this.editingId = Id;
            }
        },
        async saveSellersFields(Id, Name, Size, Location) {
            if (!await this.validateSize(Size)) {
                alert('Size must be a valid number greater than 0 and not contain letters!');
                return; // Якщо валідація не пройшла, не відправляємо дані на бек
            }

            let inputs = Array.from(await document.getElementById(Id).getElementsByTagName('input'));
            inputs.forEach(el => el.disabled = true);

            this.isEditing = false;
            this.editingId = '';

            await fieldsAPI.changeField(Id, this.sellerId, Name, Size, Location, this.UserTocken);
            this.getSellersFields();
        },
        async deleteSellersFields(Id) {
            await fieldsAPI.deleteField(Id, this.UserTocken);
            await this.getSellersFields();
        },
        async resetValues() {
            this.Name = '';
            this.Size = '';
            this.Location = '';
        },
        async validateInput(field) {
            const value = this[field];
            const validValue = value.replace(/[^0-9]/g, ''); // Видаляємо все, що не є числом
            if (validValue !== value) {
                this[field] = validValue;
            }
        },
        // Перевірка валідності Size
        async validateSize(Size) {
            // Перевірка на наявність літер чи дробових чисел
            const regex = /^[0-9]+(\.[0-9]+)?$/; // Допускається лише ціле або дробове число
            if (!regex.test(Size)) {
                return false;
            }
            const sizeValue = parseFloat(Size);
            return sizeValue > 0; // Перевірка на числове значення більше 0
        }
    },
    mounted: function () {
        this.getSellersFields();
    }
};
</script>
