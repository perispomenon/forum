const Sequelize = require('sequelize')
const sequelize = new Sequelize('postgres://xkfkngnn:pw4tICDBCrbJOCq48vQkcgaZQUhjFn63@dumbo.db.elephantsql.com:5432/xkfkngnn', {
  dialect: 'postgres',
  pool: {
    max: 9,
    min: 0,
    idle: 10000
  },
  define: {
    timestamps: true
  }
})

sequelize.authenticate()
  .then(() => {
    console.log('connected to db')
  })
  .catch(err => {
    console.log(err)
  })
