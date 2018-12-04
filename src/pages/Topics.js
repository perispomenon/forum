import React from 'react'
// import { connect } from 'react-redux'
// import autoBind from 'react-autobind'

import Topic from '../components/Topic'

// todo test data
const topics = [
  { title: 'topic1', author: 'Vasya', createdAt: new Date(), tags: ['tag11', 'tag12'], shortText: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Fuga quibusdam optio illum laudantium nobis corrupti porro commodi similique, nihil impedit. Laboriosam quod facere ipsa itaque harum repudiandae dignissimos unde quasfffffff aaaaaaaaaaaa.' },
  { title: 'topic2', author: 'Petya', createdAt: null, tags: ['tag2dddddddddddddddddddddddddddddddddddddddddddddddddddddd1', 'tag22', 'tag23'], shortText: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Fuga quibusdam optio illum laudantium nobis corrupti porro commodi similique, nihil impedit. Laboriosam quod facere ipsa itaque harum repudiandae dignissimos unde quas.' }
]

class Topics extends React.Component {
  render () {
    return (
      <div>
        <h1>All topics</h1>
        <ul>
          {topics.map(t => <Topic key={t.title} data={t}></Topic>)}
        </ul>
      </div>
    )
  }
}

export default Topics
