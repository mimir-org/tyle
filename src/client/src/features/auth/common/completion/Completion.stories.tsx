import { Meta, StoryFn } from "@storybook/react";
import { Completion } from "features/auth/common/completion/Completion";

export default {
  title: "Features/Auth/Common/Completion",
  component: Completion,
} as Meta<typeof Completion>;

const Template: StoryFn<typeof Completion> = (args) => <Completion {...args} />;

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
