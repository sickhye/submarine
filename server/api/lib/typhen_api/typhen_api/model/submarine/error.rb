# This file was generated by typhen-api

module TyphenApi::Model::Submarine
  class Error
    include Virtus.model(:strict => true)

    attribute :code, Integer, :required => true
    attribute :name, String, :required => true
    attribute :message, String, :required => true
  end
end
