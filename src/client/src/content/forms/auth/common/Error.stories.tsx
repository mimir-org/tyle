import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Error } from "./Error";

export default {
  title: "Content/Forms/Auth/Common/Error",
  component: Error,
} as ComponentMeta<typeof Error>;

const Template: ComponentStory<typeof Error> = (args) => <Error {...args} />;

export const Default = Template.bind({});
Default.args = {
  children:
    "We were not able verify your ownership of this email. Please try again in about a minute. If the issue persist ",
};
