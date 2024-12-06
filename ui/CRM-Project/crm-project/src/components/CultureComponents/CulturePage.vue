<template>
    <button @click="currentPage = 'main'">Main</button>
    <div class="modal-body">
        <div id="createCulture">
            <select v-model="Name">
                <option value="" disabled>Choose culture name</option>
                <option value="0">Grain</option>
                <option value="1">Legume</option>
                <option value="2">Oilseed</option>
                <option value="3">Root</option>
                <option value="4">Fruit</option>
                <option value="5">Vegetable</option>
                <option value="6">Green</option>
                <option value="7">Berry</option>
                <option value="8">Technical</option>
                <option value="9">Fodder</option>
                <option value="10">Beverage</option>
                <option value="11">Spice</option>
            </select>
            <input type="text" placeholder="Your Culture Amount" v-model="Amount">
            <input type="text" :placeholder=plaseholderHarvestDate @focus="plaseholderHarvestDate = '(MM.DD.YYYY)'"
                @blur="plaseholderHarvestDate = 'Your Culture Harvest Date'" v-model="HarvestDate"
                v-mask="'##.##.####'">
            <button @click="createSellersCultures()">Record Culture</button>
        </div>
        <h1>Your Crops:</h1>
        <table>
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Amount</th>
                    <th>Harvest Date</th>
                    <th>Change/Delete</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="culture in Cultures.filter(row => row.OwnerId == sellerId)" :key="culture.Id"
                    :id="culture.Id">
                    <td><input id="Name" type="text" v-model="culture.Name" disabled></td>
                    <td><input id="Amount" type="text" v-model="culture.Amount" disabled></td>
                    <td><input id="HarvestDate" type="text" v-model="culture.HarvestDate" v-mask="'####.##.##'"
                            disabled></td>
                    <td>
                        <button @click="changeSellersCultures(culture.Id)"
                            v-if="culture.Id != editingId">Change</button>
                        <button
                            @click="saveSellersCultures(culture.Id, culture.Name, culture.Amount, culture.HarvestDate)"
                            v-if="culture.Id == editingId">Save</button>
                        <button @click="deleteSellersCultures(culture.Id)">Delete</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<style scoped>
.modal-body {
    padding: 20px;
    max-width: 800px;
    margin: 20px auto;
    background-color: #ffffff;
    border-radius: 10px;
    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
    border: 1px solid #e0e0e0;
}

#createCulture {
    margin-bottom: 20px;
    display: flex;
    flex-direction: column;
    gap: 15px;
}

select,
input[type="text"],
button {
    padding: 10px;
    font-size: 14px;
    border-radius: 5px;
    border: 1px solid #cccccc;
    width: 100%;
    box-sizing: border-box;
}

select:focus,
input[type="text"]:focus {
    outline: none;
    border-color: #007bff;
    background-color: #f9f9f9;
}

button {
    background-color: #007bff;
    color: #ffffff;
    border: none;
    cursor: pointer;
    transition: background-color 0.3s;
}

button:hover {
    background-color: #0056b3;
}

button:active {
    background-color: #004085;
}

h1 {
    text-align: center;
    font-size: 1.5rem;
    margin-bottom: 15px;
    color: #333333;
}

table {
    width: 100%;
    border-collapse: collapse;
    margin-top: 20px;
}

table th,
table td {
    text-align: left;
    padding: 10px;
    border: 1px solid #e0e0e0;
}

table th {
    background-color: #f5f5f5;
    font-weight: bold;
}

table tbody tr:nth-child(even) {
    background-color: #f9f9f9;
}

table tbody tr:hover {
    background-color: #f1f1f1;
}

table button {
    padding: 5px 10px;
    font-size: 12px;
}

button:disabled {
    background-color: #cccccc;
    cursor: not-allowed;
}
</style>


<script>
import culturesAPI from '../../culturesAPI.js';
export default {
    props: ['localCurrentPage', 'localSellerId', 'localToken'],
    components: {
    },
    data() {
        return {
            Cultures: [],
            plaseholderHarvestDate: 'Your Culture Harvest Date',
            currentPage: this.localCurrentPage,
            sellerId: this.localSellerId,
            Name: '',
            Amount: '',
            HarvestDate: '',
            isEditing: false,
            editingId: '',
            UserToken: this.localToken,
            cultureDictionary: {
                0: 'Grain',
                1: 'Legume',
                2: 'Oilseed',
                3: 'Root',
                4: 'Fruit',
                5: 'Vegetable',
                6: 'Green',
                7: 'Berry',
                8: 'Technical',
                9: 'Fodder',
                10: 'Beverage',
                11: 'Spice'
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
        async getSellersCultures() {
            this.Cultures = (await culturesAPI.refreshData(this.UserToken)).slice();
            this.convertCultures();
        },
        async createSellersCultures() {
            // Валідація Amount перед збереженням
            let Amount = this.validateAmount(this.Amount);
            // Валідація HarvestDate перед створенням
            if (this.validateDate(this.HarvestDate)) {
                await culturesAPI.createCulture(this.sellerId, this.Name, Amount, this.HarvestDate, this.UserToken)
                this.resetValues();
                await this.getSellersCultures();
            }

        },
        async changeSellersCultures(Id) {
            if (this.isEditing) {
                alert('Save first')
            }
            else {
                let inputs = Array.from(await document.getElementById(Id).getElementsByTagName('input'));
                inputs = inputs.filter(el => el.getAttribute('id') !== 'HarvestDate')
                inputs = inputs.filter(el => el.getAttribute('id') !== 'Name')
                inputs.forEach(el => el.disabled = false);

                this.isEdit = true;
                this.editingId = Id;
            }
        },
        async saveSellersCultures(Id, Name, Amount, HarvestDate) {
            // Валідація Amount перед збереженням
            let validateAmount = await this.validateAmount(Amount);
            let inputs = Array.from(await document.getElementById(Id).getElementsByTagName('input'));
            inputs.forEach(el => el.disabled = true);

            this.isEdit = false;
            this.editingId = '';
            let reversedName = await this.reverseCultures(Name)

            await culturesAPI.changeCulture(Id, this.sellerId, reversedName, validateAmount, HarvestDate, this.UserToken);
            await this.getSellersCultures();

        },
        async deleteSellersCultures(Id) {
            await culturesAPI.deleteCulture(Id, this.UserToken);
            await this.getSellersCultures();
        },
        async resetValues() {
            this.Name = '';
            this.Amount = '';
            this.HarvestDate = '';
        },
        async convertCultures() {
            for (let i = 0; i < this.Cultures.length; i++) {
                const culture = this.Cultures[i];
                // Заміна CultureId на текстове значення
                const cultureName = this.cultureDictionary[culture.Name];
                culture.Name = cultureName; // Додаємо нову властивість
            }
        },
        async reverseCultures(CultureName) {
            return Object.keys(this.cultureDictionary).find(key => this.cultureDictionary[key] === CultureName);
        },
        // Метод валідації Amount
        async validateAmount(amount) {
            return (String(amount).replace(/[^\d]/g, '')); // Видаляємо всі нецифрові символи
        },
        // Метод валідації дати
        async validateDate(date) {
            // Перевірка формату дати MM.DD.YYYY
            const regex = /^(\d{2})\.(\d{2})\.(\d{4})$/;
            const match = date.match(regex);
            if (match) {
                const month = parseInt(match[1], 10); // Отримуємо місяць
                const day = parseInt(match[2], 10);   // Отримуємо день
                const year = parseInt(match[3], 10); // Отримуємо рік

                // Перевірка діапазону місяців та днів
                if (month < 1 || month > 12 || day < 1 || day > 31) {
                    alert('Invalid date: Month or day out of range');
                    return false;
                }

                // Створюємо об'єкт дати
                const validDate = new Date(year, month - 1, day);

                // Перевіряємо, чи об'єкт дати відповідає введенню
                if (
                    validDate.getFullYear() === year &&
                    validDate.getMonth() + 1 === month &&
                    validDate.getDate() === day
                ) {
                    return true; // Дата валідна
                } else {
                    alert('Invalid date');
                    return false;
                }
            } else {
                alert('Invalid date format');
                return false;
            }
        }
    },
    mounted: function () {
        this.getSellersCultures();
    }
};
</script>