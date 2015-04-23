This project uses T4 template to generate WCF client proxy.

Knowledge:

1. Use proxy that enables caching
(http://blogs.msdn.com/b/wenlong/archive/2007/10/27/performance-improvement-of-wcf-client-proxy-creation-and-best-practices.aspx)
As mentioned above, you can either use ChannelFactory<T>.CreateChannel to create your proxy or you can use auto-generated proxies. If you use the latter, you need to be aware of the following in order to get ChannelFactory cached:
·         Don’t use the constructors of the proxy that takes Binding as an argument.
·         Don’t access the public properties ChannelFactory, Endpoint, and ClientCredentials of the proxy.

2. Always open WCF client proxy explicitly when it is shared
(http://blogs.msdn.com/b/wenlong/archive/2007/10/26/best-practice-always-open-wcf-client-proxy-explicitly-when-it-is-shared.aspx)

3. Closing your WCF Connections properly
（http://www.codeguru.com/csharp/csharp/net30/article.php/c15941/TIP-Closing-your-WCF-Connections-properly.htm）