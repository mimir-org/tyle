import { Meta, StoryFn } from "@storybook/react";
import { AuthContent } from "features/auth/common/auth-content/AuthContent";

export default {
  title: "Features/Auth/Common/AuthContent",
  component: AuthContent,
} as Meta<typeof AuthContent>;

const Template: StoryFn<typeof AuthContent> = (args) => <AuthContent {...args} />;

export const Default = Template.bind({});
Default.args = {
  title: "Title",
  subtitle: "Subtitle",
};
