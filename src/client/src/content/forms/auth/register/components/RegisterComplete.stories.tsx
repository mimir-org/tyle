import { ComponentMeta, ComponentStory } from "@storybook/react";
import { RegisterComplete } from "./RegisterComplete";

export default {
  title: "Content/Forms/Auth/RegisterComplete",
  component: RegisterComplete,
} as ComponentMeta<typeof RegisterComplete>;

const Template: ComponentStory<typeof RegisterComplete> = (args) => <RegisterComplete {...args} />;

export const Default = Template.bind({});
Default.args = {
  title: "Register",
  infoText: "You just completed the registration process",
};

export const WithAction = Template.bind({});
WithAction.args = {
  ...Default.args,
  actionable: true,
  actionText: "Return",
  onAction: () => alert("[STORYBOOK] RegisterComplete.onAction"),
};
