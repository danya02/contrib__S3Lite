using System;

namespace S3Lite
{
    /// <summary>
    /// Settings for routing requests through an intermediate gateway while signing for the upstream
    /// <see cref="S3Client.Hostname" />.
    /// Assign to <see cref="S3Client.Gateway" /> (or pass to <see cref="S3Client.WithGateway" />) to enable gateway routing.
    ///
    /// This describes a reverse proxy / gateway: the request is sent to this host with its Host header set
    /// to this host, so the gateway is expected to rewrite the Host header to <see cref="S3Client.Hostname" />
    /// before forwarding upstream. The SigV4 signature stays bound to <see cref="S3Client.Hostname" />, so it
    /// validates once the gateway has rewritten the Host header.
    ///
    /// This is NOT a forward/HTTP (CONNECT) proxy. For a forward proxy that preserves the upstream Host
    /// end-to-end, leave this unset and supply an HttpClient configured with a WebProxy instead
    /// (see <see cref="S3Client.WithHttpClient" />).
    /// </summary>
    public class GatewayConfig
    {
        #region Public-Members

        /// <summary>
        /// Gateway hostname that requests are sent to.
        /// When null or empty, no gateway routing is performed and requests go directly to <see cref="S3Client.Hostname" />.
        /// </summary>
        public string Hostname { get; set; } = null;

        /// <summary>
        /// Gateway port that requests are sent to.
        /// When 0, the <see cref="S3Client.Port" /> value is used.
        /// </summary>
        public int Port
        {
            get
            {
                return _Port;
            }
            set
            {
                if (value < 0 || value > 65535) throw new ArgumentOutOfRangeException(nameof(Port));
                _Port = value;
            }
        }

        /// <summary>
        /// Protocol (HTTP or HTTPS) used to reach the gateway.
        /// When null, the <see cref="S3Client.Protocol" /> value is used.
        /// </summary>
        public ProtocolEnum? Protocol { get; set; } = null;

        #endregion

        #region Private-Members

        private int _Port = 0;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Instantiate.
        /// </summary>
        public GatewayConfig()
        {
        }

        #endregion
    }
}
