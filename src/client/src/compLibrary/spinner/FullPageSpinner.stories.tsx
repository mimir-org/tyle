import { Spinner } from "./Spinner";
import { FullPageSpinner } from "./FullPageSpinner";
import { ComponentMeta, ComponentStory } from "@storybook/react";

export default {
  title: "Library/Molecules/FullPageSpinner",
  component: Spinner,
} as ComponentMeta<typeof FullPageSpinner>;

const Template: ComponentStory<typeof FullPageSpinner> = (args) => <FullPageSpinner {...args} />;

export const Default = Template.bind({});

export const WithText = Template.bind({});
WithText.args = {
  text: "Loading state",
};
