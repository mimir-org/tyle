import { Meta, StoryFn } from "@storybook/react";
import { mockAspectObjectItem } from "common/utils/mocks";
import { AspectObjectPanel } from "features/explore/about/components/aspectobject/AspectObjectPanel";

export default {
  title: "Features/Explore/About/AspectObjectPanel",
  component: AspectObjectPanel,
  args: {
    ...mockAspectObjectItem(),
  },
} as Meta<typeof AspectObjectPanel>;

const Template: StoryFn<typeof AspectObjectPanel> = (args) => <AspectObjectPanel {...args} />;

export const Default = Template.bind({});
