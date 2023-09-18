import { Meta, StoryObj } from "@storybook/react";
import { Digits } from "./Digits";

const meta: Meta<typeof Digits> = {
  title: "Molecules/Digits",
  component: Digits,
};

type Story = StoryObj<typeof Digits>;

export const Default: Story = {
  render: () => <Digits onChange={() => alert("[STORYBOOK] Digits.onChange")} />,
};

export default meta;
