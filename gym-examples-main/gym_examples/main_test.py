import gym
import gym_examples

#env = gym.make("LunarLander-v2", render_mode="human")
env = gym.make('gym_examples/GridWorld-v0',render_mode='human' ,size=10)

observation, info = env.reset()

for i in range(1000):
    action = env.action_space.sample()  # agent policy that uses the observation and info
    observation, reward, terminated, truncated, info = env.step(action)

    if terminated or truncated:
        observation, info = env.reset()
    print(f'Epoch {i} : Done / Reward : {reward}')
env.close()