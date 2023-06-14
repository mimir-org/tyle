import { Meta, StoryFn } from "@storybook/react";
import { FullPageSpinner } from "complib/feedback/spinner/FullPageSpinner";
import { Spinner } from "complib/feedback/spinner/Spinner";

export default {
  title: "Feedback/FullPageSpinner",
  component: Spinner,
} as Meta<typeof FullPageSpinner>;

const Template: StoryFn<typeof FullPageSpinner> = (args) => <FullPageSpinner {...args} />;

export const Default = Template.bind({});

export const WithText = Template.bind({});
WithText.args = {
  text: "Loading state",
};
