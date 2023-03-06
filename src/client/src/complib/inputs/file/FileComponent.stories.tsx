import { ComponentMeta, ComponentStory } from "@storybook/react";
import { FileComponent, FileInfo } from "./FileComponent";

export default {
  title: "Inputs/File",
  component: FileComponent,
} as ComponentMeta<typeof FileComponent>;

const Template: ComponentStory<typeof FileComponent> = (args) => <FileComponent {...args} />;

export const Default = Template.bind({});
Default.args = {
  tooltip: "This is the tooltip",
};
