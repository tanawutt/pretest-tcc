<template>
  <div class="question">
    <label>ชื่อ-สกุล</label>&nbsp;
    <input type="text" v-model="fullname" :disabled="isSubmit">
    <div v-for="q in this.questions" :key="q.id" class="question-item">
      <div>
        <label>{{ q.id }}. {{ q.text }}</label>
      </div>
      <div class="radio-group vertical">
        <label v-for="o in q.options" :key="o.id">
          <input type="radio" :value="o.id" v-model="q.answerId" :disabled="isSubmit">{{ o.text }}
        </label>
      </div>
    </div>
    <div class="button-item">
      <button v-if="!isSubmit" v-on:click="Submit">ส่งข้อสอบ</button>
      <button v-if="isSubmit" v-on:click="Renew">สอบอีกครั้ง</button>
      <label v-if="isSubmit"> {{ fullname }} สอบได้คะแนน : {{ score }}/2</label>
      <label v-if="isAlert" style="color:red;">ไม่สามารถส่งข้อสอบได้ กรุณาตรวจทานอีกครั้ง</label>
    </div>
  </div>
</template>


<script>
import axios from 'axios';

const url = 'https://localhost:7117/Questionaire';

export default {
  name: 'HelloWorld',
  props: {
    msg: String
  },
  data() {
    return {
      isSubmit: false,
      isAlert: false,
      questions: [],
      fullname: '',
      score: 0
    }
  },
  mounted() {
    this.GetQuestionaire();
  },
  methods: {
    GetQuestionaire() {
      axios.get(url).then(r => {
        console.log(r);
        this.questions = r.data;
      });
    },
    async Submit() {
      if (this.fullname && this.questions.filter(i => i.answerId == '') == 0) {
        var mod = {
          fullname: this.fullname,
          answer: this.questions.map(i => ({ id: i.id, text: i.text, answerId: i.answerId }))
        };
        console.log(mod);

        await axios.post(url, mod).then(r => {
          this.score = r.data;
          console.log(r);
        });

        this.isSubmit = true;
        this.isAlert = false;
      }
      else { this.isAlert = true; }
    },
    Renew() {
      this.isSubmit = false;
      this.isAlert = false;
      this.score = 0;
      this.fullname = '';
      this.questions.map(i => { i.answerId = ''; });
    }
  }
}
</script>

<style scoped>
.question {
  padding: 0 30px;
  text-align: left !important;
}

.question-item {
  padding-bottom: 10px;
}

.radio-group.vertical {
  display: flex;
  flex-direction: column;
  gap: 5px;
}

button {
  background-color: dodgerblue;
  color: white;
  border: 0px;
  padding: 5px;
  width: 100px;
  font-size: 15px;
}

.button-item {
  display: flex;
  gap: 20px;
}
</style>
