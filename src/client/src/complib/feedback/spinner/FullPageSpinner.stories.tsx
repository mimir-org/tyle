import { ComponentMeta, ComponentStory } from "@storybook/react";
import { FullPageSpinner } from "complib/feedback/spinner/FullPageSpinner";
import { Spinner } from "complib/feedback/spinner/Spinner";

export default {
  title: "Feedback/FullPageSpinner",
  component: Spinner,
} as ComponentMeta<typeof FullPageSpinner>;

const Template: ComponentStory<typeof FullPageSpinner> = (args) => <FullPageSpinner {...args} />;

export const Default = Template.bind({});

export const WithText = Template.bind({});
WithText.args = {
  text: "Loading state",
};
