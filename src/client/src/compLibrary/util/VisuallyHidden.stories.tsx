import { ComponentMeta, ComponentStory } from "@storybook/react";
import { VisuallyHidden } from "./VisuallyHidden";

export default {
  title: "Library/Utils/VisuallyHidden",
  component: VisuallyHidden,
  parameters: {
    viewMode: "docs",
    previewTabs: {
      canvas: { hidden: true },
    },
    docs: {
      source: {
        state: "open",
      },
    },
  },
  args: {
    children: "Some hidden text goes here.",
  },
} as ComponentMeta<typeof VisuallyHidden>;

const Template: ComponentStory<typeof VisuallyHidden> = (args) => <VisuallyHidden {...args} />;

export const Default = Template.bind({});
