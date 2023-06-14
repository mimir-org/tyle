import { Meta, StoryFn } from "@storybook/react";
import { AuthContentHeader } from "features/auth/common/auth-content/AuthContentHeader";

export default {
  title: "Features/Auth/Common/AuthContentHeader",
  component: AuthContentHeader,
} as Meta<typeof AuthContentHeader>;

const Template: StoryFn<typeof AuthContentHeader> = (args) => <AuthContentHeader {...args} />;

export const Default = Template.bind({});
Default.args = {
  title: "Title",
  subtitle: "Subtitle",
};
