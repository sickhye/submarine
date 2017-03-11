if Module::const_defined?(:MessagePack)
  ActionDispatch::IntegrationTest.register_encoder(:msgpack,
    param_encoder: -> params { params.try(:to_msgpack) },
    response_parser: -> body { MessagePack.unpack(body) || {} }
  )
end

module TyphenApiTestHelpers
  module Integration
    def post(path, options=nil)
      options ||= {}
      options[:as] ||= TyphenApiRespondable::RENDER_FORMAT
      super(path, options)
    end
  end
end

RSpec.configure do |config|
  config.include TyphenApiTestHelpers::Integration, type: :request
end