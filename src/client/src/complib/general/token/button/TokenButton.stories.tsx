import { Meta, StoryFn } from "@storybook/react";
import { TokenButton } from "complib/general/token/button/TokenButton";

export default {
  title: "General/TokenButton",
  component: TokenButton,
} as Meta<typeof TokenButton>;

const Template: StoryFn<typeof TokenButton> = (args) => <TokenButton {...args} />;

export const Primary = Template.bind({});
Primary.args = {
  children: "Primary token (default)",
  variant: "primary",
  onClick: () => alert("[STORYBOOK] TokenButton.onClick"),
};

export const Secondary = Template.bind({});
Secondary.args = {
  children: "Secondary token",
  variant: "secondary",
  onClick: () => alert("[STORYBOOK] TokenButton.onClick"),
};
