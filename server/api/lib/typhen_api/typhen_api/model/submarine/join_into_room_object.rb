# This file was generated by typhen-api

module TyphenApi::Model::Submarine
  class JoinIntoRoomObject
    include Virtus.model(:strict => true)

    attribute :room, TyphenApi::Model::Submarine::JoinedRoom, :required => true
  end
end
