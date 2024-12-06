<template>
    <div>
        <p>Please select your sex:</p>
        <input id="newMaleSex" type="radio" name="sex" value="Male" v-model="localSex" @input="validateInput"
            required />
        <label for="male">Male</label>
        <br />
        <input id="newFemaleSex" type="radio" name="sex" value="Female" v-model="localSex" @input="validateInput"
            required />
        <label for="female">Female</label>
        <br /><br />
    </div>
</template>

<script>
export default {
    props: {
        sex: {
            type: String,
            required: true,
        },
    },
    data() {
        return {
            localSex: this.sex, // Створюємо локальну змінну для двостороннього зв'язку
        };
    },
    watch: {
        // Стежимо за змінами пропса
        sex(newSex) {
            this.localSex = newSex; // Оновлюємо локальну змінну, коли змінюється пропс
        },
        localSex(newLocalSex) {
            this.$emit('update:sex', newLocalSex); // Випромінюємо оновлене значення sex
        }
    },
    methods: {
        validateInput() {
            let sexInputs = document.getElementsByName("sex");
            let chosedSex;

            for (let i = 0; i < sexInputs.length; i++) {
                if (sexInputs[i].checked) {
                    chosedSex = sexInputs[i].value;
                }
            }

            this.localSex = chosedSex;
        },
    },
};
</script>
