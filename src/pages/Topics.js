import React from 'react'
// import { connect } from 'react-redux'
// import autoBind from 'react-autobind'

import Topic from '../components/Topic'

// todo test data
const topics = [
  { id: '260089ed-da11-409b-a2a7-408479612105', title: 'topic1', author: 'Vasya', createdAt: new Date(), tags: ['tag11', 'tag12'], shortText: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Fuga quibusdam optio illum laudantium nobis corrupti porro commodi similique, nihil impedit. Laboriosam quod facere ipsa itaque harum repudiandae dignissimos unde quasfffffff aaaaaaaaaaaa.' },
  { id: 'c48de92c-d066-4811-b938-c08defbc3d89', title: 'topic2', author: 'Petya', createdAt: null, tags: ['tag2dddddddddddddddddddddddddddddddddddddddddddddddddddddd1', 'tag22', 'tag23'], shortText: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Fuga quibusdam optio illum laudantium nobis corrupti porro commodi similique, nihil impedit. Laboriosam quod facere ipsa itaque harum repudiandae dignissimos unde quas.' }
]

class Topics extends React.Component {
  render () {
    return (
      <div style={{ marginBottom: 30 }}>
        <h1>All topics</h1>
        <ul>
          {topics.map(t => <Topic key={t.title} data={t}></Topic>)}
        </ul>
      </div>
    )
  }
}

export default Topics
