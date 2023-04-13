import { ComponentMeta, ComponentStory } from "@storybook/react";
import { mockAspectObjectItem } from "common/utils/mocks";
import { AspectObjectPanel } from "features/explore/about/components/aspectobject/AspectObjectPanel";

export default {
  title: "Features/Explore/About/AspectObjectPanel",
  component: AspectObjectPanel,
  args: {
    ...mockAspectObjectItem(),
  },
} as ComponentMeta<typeof AspectObjectPanel>;

const Template: ComponentStory<typeof AspectObjectPanel> = (args) => <AspectObjectPanel {...args} />;

export const Default = Template.bind({});
