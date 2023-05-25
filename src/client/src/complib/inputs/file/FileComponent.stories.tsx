import { Meta, StoryFn } from "@storybook/react";
import { FileComponent } from "./FileComponent";

export default {
  title: "Inputs/File",
  component: FileComponent,
} as Meta<typeof FileComponent>;

const Template: StoryFn<typeof FileComponent> = (args) => <FileComponent {...args} />;

export const Default = Template.bind({});
Default.args = {
  tooltip: "This is the tooltip",
  value: null,
};
