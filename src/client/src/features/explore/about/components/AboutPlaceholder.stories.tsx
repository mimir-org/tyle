import { StoryFn } from "@storybook/react";
import { AboutPlaceholder } from "features/explore/about/components/AboutPlaceholder";

export default {
  title: "Features/Explore/About/AboutPlaceholder",
  component: AboutPlaceholder,
};

const Template: StoryFn<typeof AboutPlaceholder> = (args) => <AboutPlaceholder {...args}></AboutPlaceholder>;

export const Default = Template.bind({});
Default.args = {
  text: "Select an item to view its properties",
};
