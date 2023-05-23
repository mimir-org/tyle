import { Meta, StoryFn } from "@storybook/react";
import { Error } from "features/auth/common/error/Error";

export default {
  title: "Features/Auth/Common/Error",
  component: Error,
} as Meta<typeof Error>;

const Template: StoryFn<typeof Error> = (args) => <Error {...args} />;

export const Default = Template.bind({});
Default.args = {
  children:
    "We were not able verify your ownership of this email. Please try again in about a minute. If the issue persist ",
};
