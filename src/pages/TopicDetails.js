import React from 'react'
import PropTypes from 'prop-types'

const topic = { id: '260089ed-da11-409b-a2a7-408479612105', title: 'topic1', author: 'Vasya', createdAt: new Date(), tags: ['tag11', 'tag12'], shortText: 'Lorem ipsum dolor sit amet consectetur adipisicing elit. Fuga quibusdam optio illum laudantium nobis corrupti porro commodi similique, nihil impedit. Laboriosam quod facere ipsa itaque harum repudiandae dignissimos unde quasfffffff aaaaaaaaaaaa.' }

class TopicDetails extends React.Component {
  render () {
    // const topic = this.props.topic
    return (
      <div>
        <h1>{topic.title}</h1>
        <div className="topicText">
          <p>{topic.fullText}</p>
        </div>
      </div>
    )
  }
}

TopicDetails.propTypes = {
  topic: PropTypes.object
}

export default TopicDetails
