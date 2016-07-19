package battle

import (
	"app/battle/context"
	"time"
)

type judge struct {
	context   *context.Context
	timeLimit time.Duration
}

func newJudge(context *context.Context, timeLimit time.Duration) *judge {
	return &judge{
		context:   context,
		timeLimit: timeLimit,
	}
}

func (j judge) isBattleFinished() bool {
	return j.context.ElapsedTime() >= j.timeLimit || len(j.context.Users()) == 1
}

func (j judge) winner() *context.User {
	users := j.context.Users()
	if len(users) != 1 {
		return nil
	}
	return users[0]
}