import { ComponentMeta, ComponentStory } from "@storybook/react";
import { TokenButton } from "complib/general/token/TokenButton";

export default {
  title: "General/TokenButton",
  component: TokenButton,
} as ComponentMeta<typeof TokenButton>;

const Template: ComponentStory<typeof TokenButton> = (args) => <TokenButton {...args} />;

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
