import { ComponentMeta, ComponentStory } from "@storybook/react";
import { mockTransportItem } from "common/utils/mocks";
import { TransportPanel } from "features/explore/about/components/transport/TransportPanel";

export default {
  title: "Explore/About/TransportPanel",
  component: TransportPanel,
  args: {
    ...mockTransportItem(),
  },
} as ComponentMeta<typeof TransportPanel>;

const Template: ComponentStory<typeof TransportPanel> = (args) => <TransportPanel {...args} />;

export const Default = Template.bind({});
