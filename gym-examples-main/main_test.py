import gymnasium as gym
import gym_examples

#env = gym.make("LunarLander-v2", render_mode="human")
env = gymnasium.make('gym_examples/GridWorld-v0', size=10)

observation, info = env.reset()

for _ in range(1000):
    action = env.action_space.sample()  # agent policy that uses the observation and info
    observation, reward, terminated, truncated, info = env.step(action)

    if terminated or truncated:
        observation, info = env.reset()

env.close()