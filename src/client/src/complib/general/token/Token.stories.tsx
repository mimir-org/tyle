import { ComponentMeta, ComponentStory } from "@storybook/react";
import { LibraryIcon } from "complib/assets";
import { Token } from "./Token";

export default {
  title: "General/Token",
  component: Token,
} as ComponentMeta<typeof Token>;

const Template: ComponentStory<typeof Token> = (args) => <Token {...args} />;

export const Primary = Template.bind({});
Primary.args = {
  children: "Primary token (default)",
  variant: "primary",
};

export const Secondary = Template.bind({});
Secondary.args = {
  children: "Secondary token",
  variant: "secondary",
};

export const Actionable = Template.bind({});
Actionable.args = {
  children: "Actionable token",
  actionable: true,
  actionIcon: LibraryIcon,
  actionText: "Trigger action",
  onAction: () => alert("[STORYBOOK] Token.onAction"),
};
