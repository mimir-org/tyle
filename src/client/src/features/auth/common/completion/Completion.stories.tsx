import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Completion } from "features/auth/common/completion/Completion";

export default {
  title: "Auth/Common/Completion",
  component: Completion,
} as ComponentMeta<typeof Completion>;

const Template: ComponentStory<typeof Completion> = (args) => <Completion {...args} />;

export const Default = Template.bind({});
Default.args = {
  title: "Register",
  infoText: "You just completed the registration process",
};

export const WithAction = Template.bind({});
WithAction.args = {
  ...Default.args,
  complete: {
    actionable: true,
    actionText: "Return",
    onAction: () => alert("[STORYBOOK] Completion.onAction"),
  },
};
